output "host_url" {
  value = "https://${azurerm_container_app.container_app.ingress[0].fqdn}"
}

output "name" {
  value = azurerm_container_app.container_app.name
}
