output "server_fqdn" {
  description = "Fully qualified hostname the backend uses to connect"
  value       = azurerm_postgresql_flexible_server.postgresql_flexible_server.fqdn
}

output "database_name" {
  description = "Name of the application database"
  value       = azurerm_postgresql_flexible_server_database.postgresql_flexible_server_database.name
}

output "admin_login" {
  description = "Admin username for the PostgreSQL server"
  value       = azurerm_postgresql_flexible_server.postgresql_flexible_server.administrator_login
}