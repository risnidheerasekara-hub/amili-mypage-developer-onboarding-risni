output "server_fqdn" {
  value = azurerm_postgresql_flexible_server.server.fqdn
}

output "database_name" {
  value = azurerm_postgresql_flexible_server_database.database.name
}

output "admin_login" {
  value = azurerm_postgresql_flexible_server.server.administrator_login
}
