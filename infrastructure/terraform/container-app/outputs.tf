output "default_hostname" {
  value = azurerm_container_app.container_app.latest_revision_fqdn
}

output "name" {
  value = azurerm_container_app.container_app.name
}
