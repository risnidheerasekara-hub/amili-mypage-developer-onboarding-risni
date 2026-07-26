output "default_hostname" {
  value = azurerm_container_app.app.latest_revision_fqdn
}

output "name" {
  value = azurerm_container_app.app.name
}
