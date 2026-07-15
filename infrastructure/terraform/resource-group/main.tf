resource "azurerm_resource_group" "resource_group" {
  name     = "${var.prefix}-${var.environment}-${var.service}-rg"
  location = var.location
  tags     = var.tags
}
