variable "prefix" {
  description = "prefix name"
}

variable "environment" {
  description = "environment name"
}

variable "service" {
  description = "service name"
}

variable "container_app_env_id" {
  description = "Container App Environment ID"
}

variable "location" {
  description = "location name"
}

variable "resource_group_name" {
  description = "azure resource group name"
}

variable "image" {
  description = "Container image to deploy"
}

variable "registry_server" {
  description = "Container registry login server"
}

variable "registry_username" {
  description = "Container registry admin username"
  sensitive   = true
}

variable "registry_password" {
  description = "Container registry admin password"
  sensitive   = true
}

variable "app_settings" { type = map(string) }

variable "tags" { type = map(string) }
