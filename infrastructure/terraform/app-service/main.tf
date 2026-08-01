resource "azurerm_linux_web_app" "linux_web_app" {
  name                = "${var.prefix}-${var.environment}-${var.service}-app"
  resource_group_name = var.resource_group_name
  location            = var.location                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
  service_plan_id     = var.service_plan_id
  tags                = var.tags

  site_config {
    app_command_line = var.app_command_line

    application_stack {
      dotnet_version = "9.0"
    }
  }

  app_settings = var.app_settings
}
