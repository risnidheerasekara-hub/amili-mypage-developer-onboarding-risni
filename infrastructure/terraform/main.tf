locals {
  tags = {
    product     = var.prefix
    environment = var.environment
    service     = var.service
    location         = var.location
    created_by       = var.created_by
    provisioned_with = var.provisioned_with
  }

  database_connection_string = "Host=${module.postgres_server.server_fqdn};Port=5432;Database=${module.postgres_server.database_name};Username=${module.postgres_server.admin_login};Password=${random_password.postgres_admin_password.result};"
}

resource "random_password" "postgres_admin_password" {
  length           = 24
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
  min_lower        = 2
  min_upper        = 2
  min_numeric      = 2
  min_special      = 2
}

module "resource_group" {
  source      = "./resource-group"
  prefix      = var.prefix
  environment = var.environment
  service     = var.service
  location    = var.location
  tags        = local.tags
}

module "static_web_app" {
  source              = "./static-web-app"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.azure-rg-name
  sku_tier            = var.swa_sku_tier
  tags                = local.tags
}

module "app_service_plan" {
  source              = "./app-service-plan"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.azure-rg-name
  os_type             = var.os_type
  asp_sku_name        = var.asp_sku_name
  tags                = local.tags
}

module "postgres_server" {
  source               = "./postgres-server"  
  prefix               = var.prefix
  environment          = var.environment
  service              = var.service
  location             = var.location
  resource_group_name  = module.resource_group.azure-rg-name
  administrator_login  = var.postgres_admin_login
  administrator_password = random_password.postgres_admin_password.result
  postgresql_version   = var.postgresql_version
  postgresql_sku       = var.postgresql_sku
  storage_mb           = var.postgres_storage_mb
  tags                 = local.tags
}

module "app_service" {
  source              = "./app-service"
  name                = "${var.prefix}-${var.environment}-${var.service}-api"
  location            = var.location
  resource_group_name = module.resource_group.azure-rg-name
  asp_id              = module.app_service_plan.asp_id
  tags                = local.tags

  app_settings = {
    "DATABASE_URL"           = local.database_connection_string
    "ASPNETCORE_ENVIRONMENT" = var.environment
    "WEBSITE_RUN_FROM_PACKAGE" = "1"
  }
}


