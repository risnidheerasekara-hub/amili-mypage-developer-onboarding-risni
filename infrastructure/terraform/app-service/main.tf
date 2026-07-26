resource "azurerm_linux_web_app" "app" {
  name                = var.name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = var.asp_id
  tags                = var.tags

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
  }

  app_settings = var.app_settings
}