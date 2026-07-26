resource "azurerm_resource_group" "rg" {
  name     = "${var.prefix}-${var.environment}-${var.service}-rg"
  location = var.location
  tags = var.tags
}