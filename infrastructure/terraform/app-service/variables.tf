variable "name" {}
variable "resource_group_name" {}
variable "location" {}
variable "service_plan_id" {}
variable "app_settings" { type = map(string) }
variable "runtime" {
  description = "\"dotnet\" or \"node\""
}
variable "app_command_line" {
  default = ""
}
variable "tags" { type = map(string) }
