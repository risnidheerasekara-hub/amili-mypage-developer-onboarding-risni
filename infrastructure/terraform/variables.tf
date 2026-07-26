
variable "os_type" {
  description = "OS for the App Service Plan: Linux or Windows"
  type        = string
  default     = "Linux"
}

variable "asp_sku_name" {
  description = "SKU for the App Service Plan, e.g. B1, P1v3"
  type        = string
  default     = "B1"
}

variable "postgres_admin_login" {
  description = "PostgreSQL administrator username"
  type        = string
  sensitive   = true
  default = "todouser"
}


variable "postgresql_version" {
  description = "PostgreSQL version, e.g. '15'"
  type        = string
  default     = "15"
}

variable "postgresql_sku" {
  description = "PostgreSQL compute SKU, e.g. B_Standard_B1ms"
  type        = string
  default     = "B_Standard_B1ms"
}

variable "postgres_storage_mb" {
  description = "PostgreSQL storage in MB"
  type        = number
  default     = 32768
}

variable "swa_sku_tier" {
  description = "Static Web App SKU tier: Free or Standard"
  type        = string
  default     = "Free"
}

variable "created_by" {
  description = "created by whome"
  default = "ris"
}