
variable "swa_location" {
  description = "Location for Static Web App (limited regions: centralus, eastus2, westus2, westeurope, eastasia)"
  type        = string
  default     = "westeurope"
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