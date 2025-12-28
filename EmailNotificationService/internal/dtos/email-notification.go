package dtos

import (
	"time"

	"github.com/google/uuid"
)

type EmailNotificationDto struct {
	Id          uuid.UUID         `json:"Id"`
	Email       string            `json:"Email"`
	Theme       string            `json:"Theme"`
	Message     string            `json:"Message"`
	Attachments map[string]string `json:"Attachments,omitempty"`
	CreatedAt   time.Time         `json:"CreatedAt"`
}
