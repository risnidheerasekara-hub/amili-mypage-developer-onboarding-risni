variable "asp_id" {
  description = "azure service plan id"
}

variable "location" {
  description = "location name"
}

variable "name" {
    description = "app service name"
}
variable "resource_group_name" {
  description = "azure resource group name"
}

variable "app_settings" { type = map(string) }

variable "tags" { type = map(string) }