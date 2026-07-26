output "default_hostname" {
  value = azurerm_static_web_app.frontend.default_host_name
}

output "deployment_token" {
  value     = azurerm_static_web_app.frontend.api_key
  sensitive = true
}

