variable "resource_group_name" { 
    description = "resource group name"
 }
variable "sku_tier"            { 
    description = "sku tier"
    default = "Free" 
 }

variable "tags" { 
    type = map(string) 
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