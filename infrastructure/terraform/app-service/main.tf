resource "azurerm_linux_web_app" "app" {
  name                = var.name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = var.service_plan_id
  tags                = var.tags

  site_config {
    app_command_line = var.app_command_line

    application_stack {
      dotnet_version = var.runtime == "dotnet" ? "9.0" : null
      node_version    = var.runtime == "node" ? "20-lts" : null
    }
  }

  app_settings = var.app_settings
}
