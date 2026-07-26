
resource "azurerm_postgresql_flexible_server" "postgresql_server" {
  name                          = "${var.prefix}-${var.environment}-${var.service}-postgresql"
  resource_group_name           = var.resource_group_name
  location                      = var.location
  version                       = var.postgresql_version
  administrator_login           = var.administrator_login
  administrator_password        = var.administrator_password
  zone                          = "2"
  public_network_access_enabled = true
  tags                          = var.tags
  storage_mb                    = var.storage_mb
  sku_name                      = var.postgresql_sku
}

resource "azurerm_postgresql_flexible_server_database" "app_db" {
  name      = "${var.prefix}_${var.environment}_db"
  server_id = azurerm_postgresql_flexible_server.postgresql_server.id
  collation = "en_US.utf8"
  charset   = "utf8"
}
