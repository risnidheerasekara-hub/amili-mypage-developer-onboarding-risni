output "login_server" {
  value = azurerm_container_registry.container_registry.login_server
}

output "admin_username" {
  value     = azurerm_container_registry.container_registry.admin_username
  sensitive = true
}

output "admin_password" {
  value     = azurerm_container_registry.container_registry.admin_password
  sensitive = true
}
