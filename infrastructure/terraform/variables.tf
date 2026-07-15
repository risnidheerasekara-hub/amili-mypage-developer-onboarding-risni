variable "subscription" {
  description = "Azure subscription id"
}

variable "prefix" {
  description = "Naming prefix"
  default     = "amili"
}

variable "environment" {
  description = "Environment name"
  default     = "dev"
}

variable "service" {
  description = "Service name"
  default     = "todo"
}

variable "location" {
  description = "Azure region"
  default     = "westeurope"
}

variable "postgres_admin_password" {
  description = "Postgres administrator password"
  sensitive   = true
}

provider "azurerm" {
  subscription_id = var.subscription
  features {}
}

terraform {
  required_version = ">=1.5.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">=4.0.0"
    }
  }
}
