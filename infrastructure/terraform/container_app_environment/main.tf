resource "azurerm_container_app_environment" "env" {
  name                = "${var.prefix}-${var.environment}-${var.service}-cae"
  location            = var.location
  resource_group_name = var.resource_group_name
  tags                = var.tags
}
