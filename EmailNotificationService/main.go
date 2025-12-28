package main

import (
	"email-notification-service/internal/services"
	"log"
	"os"
	"os/signal"
	"syscall"

	amqp "github.com/rabbitmq/amqp091-go"
)

func main() {
	cfg, err := services.InitializeConfig()
	failOnError(err, "Failed to get config")

	conn, err := amqp.Dial(cfg.RabbitMQUrl)
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	defer ch.Close()

	// Declare send queue
	_, err = ch.QueueDeclare(
		"notifications.email.send", // name
		true,                       // durable
		false,                      // delete when unused
		false,                      // exclusive
		false,                      // no-wait
		nil,                        // arguments
	)
	failOnError(err, "Failed to declare send queue")

	// Declare result queue
	_, err = ch.QueueDeclare(
		"notifications.email.result", // name
		true,                         // durable
		false,                        // delete when unused
		false,                        // exclusive
		false,                        // no-wait
		nil,                          // arguments
	)
	failOnError(err, "Failed to declare result queue")

	// Consume из send queue
	msgs, err := ch.Consume(
		"notifications.email.send", // queue
		"",                         // consumer
		true,                       // auto-ack
		false,                      // exclusive
		false,                      // no-local
		false,                      // no-wait
		nil,                        // args
	)
	failOnError(err, "Failed to register a consumer")

	log.Println("Email service started. Waiting for messages...")

	// Graceful shutdown
	sigchan := make(chan os.Signal, 1)
	signal.Notify(sigchan, syscall.SIGINT, syscall.SIGTERM)

	go func() {
		for d := range msgs {
			services.ProcessMessage(ch, d.Body, cfg)
		}
	}()

	<-sigchan
	log.Println("Shutting down...")
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Fatalf("%s: %v", msg, err)
		panic(err)
	}
}
