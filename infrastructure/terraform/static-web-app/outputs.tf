output "default_hostname" {
  value = azurerm_static_web_app.static_web_app.default_host_name
}

output "deployment_token" {
  value     = azurerm_static_web_app.static_web_app.api_key
  sensitive = true
}

