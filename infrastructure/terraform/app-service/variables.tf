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
  description = "app service location"
}

variable "service_plan_id" {
  description = "app service plan id"
}

variable "app_settings" { type = map(string) }

variable "app_command_line" {
  default = ""
}

variable "tags" {
  description = "required tags"
}

variable "resource_group_name" {
  description = "azure resource group name"
}
