terraform {
  required_version = ">= 1.5"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.100"
    }
  }
}

provider "azurerm" {
  subscription_id                 = var.subscription
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}


variable "tenant_id" {
  description = "Azure Tenant ID"
  default = "99x"
}

variable "subscription" {
  description = "Azure Subscription ID"
  default = "ef57fb40-492d-49b5-9af6-f7da332b9e53"
}

variable "product" {
  description = "Product name"
  default     = "mypage"
}

variable "environment" {
  description = "Environment name (dev, qa, prod)"
  default     = "dev"
}

variable "service" {
  description = "Service name, e.g. 'mypage'"
  type        = string
  default = "mypage"
}

variable "location" {
  description = "Azure region for resources"
  default     = "westeurope"
}

variable "provisioned_with" {
  description = "Provisioning tool identifier"
  default     = "terraform"
}

variable "prefix" {
  description = "Resource name prefix"
  default     = "amili"
}

