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

variable "resource_group_name" {
  description = "azure resource group name"
}

variable "tags" {
  type        = map(string)
  description = "required tags"
}
