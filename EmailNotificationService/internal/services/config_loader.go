package services

import (
	"fmt"
	"os"
	"path/filepath"
	"strings"

	"github.com/ilyakaznacheev/cleanenv"
)

type AppConfig struct {
	RabbitMQUrl string `json:"RabbitMQUrl" env:"RABBITMQURL"`
	SmtpHost    string `json:"SmtpHost" env:"SMTPHOST"`
	SmtpPort    string `json:"SmtpPort" env:"SMTPPORT"`
	SmtpUser    string `json:"SmtpUser" env:"SMTPUSER"`
	SmtpPass    string `json:"SmtpPass" env:"SMTPPASS"`
	FromEmail   string `json:"FromEmail" env:"FROMEMAIL"`
}

var cfg AppConfig
var cachedProjectRootPath string

// Initialize config - from first from config file, then from env vars (overriding). So you can choose any variant you want (even mixing)
func InitializeConfig() (*AppConfig, error) {
	workingDirectory, err := os.Getwd()
	if err != nil {
		return nil, err
	}

	// Config from appsetting.json (variant through config file)
	projectRootPath, err := findProjectRoot(workingDirectory)
	if err != nil {
		fmt.Println("didn't found project root (it depends on go.mod file) - if you're using env variables - skip this message")
	} else {
		path := projectRootPath + "/configs/appsettings.json"

		// Reading config from file
		err = cleanenv.ReadConfig(path, &cfg)
		if err != nil {
			return nil, fmt.Errorf("failed to read config file: %w", err)
		}
	}

	// Config from environment variables (variant through env)
	err = cleanenv.ReadEnv(&cfg)
	if err != nil {
		fmt.Println("can't read env variables - if you're using config file - skip this message")
	}

	// Validate required fields
	if err := validateConfig(&cfg); err != nil {
		return nil, err
	}

	fmt.Printf("%v\n", cfg)
	return &cfg, nil
}

func validateConfig(cfg *AppConfig) error {
	var missing []string

	if cfg.RabbitMQUrl == "" {
		missing = append(missing, "RabbitMQUrl")
	}
	if cfg.SmtpHost == "" {
		missing = append(missing, "SmtpHost")
	}
	if cfg.SmtpPort == "" {
		missing = append(missing, "SmtpPort")
	}
	if cfg.SmtpUser == "" {
		missing = append(missing, "SmtpUser")
	}
	if cfg.SmtpPass == "" {
		missing = append(missing, "SmtpPass")
	}
	if cfg.FromEmail == "" {
		missing = append(missing, "FromEmail")
	}

	if len(missing) > 0 {
		return fmt.Errorf("missing required config fields: %s", strings.Join(missing, ", "))
	}

	return nil
}

func findProjectRoot(startDir string) (string, error) {
	if cachedProjectRootPath != "" {
		return cachedProjectRootPath, nil
	}

	dir := startDir
	for {
		if _, err := os.Stat(filepath.Join(dir, "go.mod")); err == nil {
			cachedProjectRootPath = dir
			return dir, nil
		}

		parent := filepath.Dir(dir)
		if parent == dir {
			return "", fmt.Errorf("project root not found")
		}
		dir = parent
	}
}
