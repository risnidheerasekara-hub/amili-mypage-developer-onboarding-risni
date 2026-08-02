variable "resource_group_name" {
  description = "azure resource group name"
}

variable "prefix" {
  description = "prefix name"
}

variable "environment" {
  description = "environment name"
}

variable "service" {
  description = "service name"
}

variable "location" {
  description = "location name"
}

variable "tags" {
  description = "required tags"
}

variable "administrator_login" {
  description = "administrator username"
}

variable "administrator_password" {
  description = "administrator password"
}

variable "postgresql_version" {
  description = "postgresql server version"
}

variable "postgresql_sku" {
  description = "postgresql sku"
}

variable "storage_mb" {
  description = "max storage size in mb"
}
