output "server_fqdn" {
  description = "Fully qualified hostname the backend uses to connect"
  value       = azurerm_postgresql_flexible_server.postgresql_server.fqdn
}

output "admin_login" {
  value     = azurerm_postgresql_flexible_server.postgresql_server.administrator_login
  sensitive = true
}

output "database_name" {
  description = "Name of the application database"
  value       = azurerm_postgresql_flexible_server_database.app_db.name
}
