FROM golang:1.24.2-alpine AS builder
WORKDIR /app
COPY . .
RUN CGO_ENABLED=0 GOOS=linux go build -o email-notification-service .

FROM alpine:latest
RUN apk --no-cache add ca-certificates
WORKDIR /root/
COPY --from=builder /app/email-notification-service .
RUN chmod +x ./email-notification-service
CMD ["./email-notification-service"]