variable "prefix" {}
variable "environment" {}
variable "service" {}
variable "location" {}
variable "resource_group_name" {}
variable "admin_password" { sensitive = true }
variable "tags" { type = map(string) }
