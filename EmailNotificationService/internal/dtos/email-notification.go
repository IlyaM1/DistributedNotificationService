package dtos

import (
	"time"

	"github.com/google/uuid"
)

type EmailNotificationDto struct {
	Id          uuid.UUID         `json:"id"`
	Email       string            `json:"email"`
	Theme       string            `json:"theme"`
	Message     string            `json:"message"`
	Attachments map[string]string `json:"attachments,omitempty"`
	CreatedAt   time.Time         `json:"createdAt"`
}
