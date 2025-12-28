package services

import (
	"context"
	"email-notification-service/internal/dtos"
	"encoding/json"
	"fmt"
	"log"
	"strconv"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
	"gopkg.in/gomail.v2"
)

func ProcessMessage(ch *amqp.Channel, body []byte, config *AppConfig) {
	var emailDto dtos.EmailNotificationDto
	err := json.Unmarshal(body, &emailDto)
	if err != nil {
		log.Printf("Error unmarshalling message: %v", err)
		return
	}

	log.Printf("Received email for %s with id %s: Subject '%s', Body '%s'", emailDto.Email, emailDto.Id, emailDto.Theme, emailDto.Message)

	// Publish PROCESSING
	publishResult(ch, dtos.BrokerResponseDto{
		Id:        emailDto.Id,
		Status:    "PROCESSING",
		Timestamp: time.Now().UTC(),
	})

	// Send email
	err = sendEmail(emailDto, config)
	if err != nil {
		log.Printf("Error sending email: %v", err)
		publishResult(ch, dtos.BrokerResponseDto{
			Id:           emailDto.Id,
			Status:       "FAILED",
			ErrorMessage: fmt.Sprintf("Error sending email: %v", err),
			Timestamp:    time.Now().UTC(),
		})
		return
	}

	// Publish SENT
	publishResult(ch, dtos.BrokerResponseDto{
		Id:        emailDto.Id,
		Status:    "SENT",
		Timestamp: time.Now().UTC(),
	})
}

func sendEmail(emailDto dtos.EmailNotificationDto, config *AppConfig) error {
	message := gomail.NewMessage()
	message.SetHeader("From", cfg.FromEmail)
	message.SetHeader("To", emailDto.Email)
	message.SetHeader("Subject", emailDto.Theme)
	message.SetBody("text/plain", emailDto.Message)

	port, _ := strconv.Atoi(config.SmtpPort)
	dialer := gomail.NewDialer(config.SmtpHost, port, config.SmtpUser, config.SmtpPass)
	err := dialer.DialAndSend(message)
	return err
}

func publishResult(ch *amqp.Channel, res dtos.BrokerResponseDto) {
	body, err := json.Marshal(res)
	if err != nil {
		log.Printf("Error marshalling result: %v", err)
		return
	}

	err = ch.PublishWithContext(
		context.Background(),
		"",                           // exchange
		"notifications.email.result", // routing key
		false,                        // mandatory
		false,                        // immediate
		amqp.Publishing{
			ContentType: "application/json",
			Body:        body,
		},
	)

	if err != nil {
		log.Fatalf("%s: %v", "Failed to publish result", err)
	}

	log.Printf("Published result for %s: %s", res.Id, res.Status)
}
