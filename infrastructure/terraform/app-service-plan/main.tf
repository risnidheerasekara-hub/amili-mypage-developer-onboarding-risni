resource "azurerm_service_plan" "app_service_plan" {
  name                = "${var.prefix}-${var.environment}-${var.service}-asp"
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = var.os_type
  sku_name            = var.asp_sku_name
  tags                = var.tags
}

