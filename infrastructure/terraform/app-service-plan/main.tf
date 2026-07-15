resource "azurerm_service_plan" "plan" {
  name                = "${var.prefix}-${var.environment}-${var.service}-asp"
  resource_group_name = var.resource_group_name
  location            = var.location
  os_type             = "Linux"
  sku_name            = "B1"
  tags                = var.tags
}
