package dtos

import (
	"time"

	"github.com/google/uuid"
)

type BrokerResponseDto struct {
	Id           uuid.UUID         `json:"Id"`
	Status       string            `json:"Status"`
	ErrorMessage string            `json:"ErrorMessage,omitempty"`
	Timestamp    time.Time         `json:"Timestamp"`
	Metadata     map[string]string `json:"Metadata,omitempty"`
}
