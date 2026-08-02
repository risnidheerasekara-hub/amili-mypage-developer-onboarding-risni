resource "azurerm_container_registry" "container_registry" {
  name                = "${var.prefix}${var.environment}${var.service}registry"
  resource_group_name = var.resource_group_name
  location            = var.location
  sku                 = "Basic"
  admin_enabled       = true
  tags                = var.tags
}
