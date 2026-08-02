locals {
  tags = {
    product          = var.prefix
    environment      = var.environment
    location         = var.location
    created_by       = var.created_by
    provisioned_with = var.provisioned_with
  }

  database_connection_string = "Host=${module.postgres_server.server_fqdn};Port=5432;Database=${module.postgres_server.database_name};Username=${module.postgres_server.admin_login};Password=${random_password.postgres_admin_password.result};"
}

resource "random_password" "postgres_admin_password" {
  length           = 16
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?@"
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
  location            = var.swa_location
  resource_group_name = module.resource_group.resource_group_name
  sku_tier            = var.swa_sku_tier
  tags                = local.tags
}

module "container_registry" {
  source              = "./container-registry"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.resource_group_name
  tags                = local.tags
}

module "container_app_environment" {
  source              = "./container-app-environment"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.resource_group_name
  tags                = local.tags
}

module "postgres_server" {
  source                 = "./postgres-server"
  prefix                 = var.prefix
  environment            = var.environment
  service                = var.service
  location               = var.location
  resource_group_name    = module.resource_group.resource_group_name
  administrator_login    = var.postgres_admin_login
  administrator_password = random_password.postgres_admin_password.result
  postgresql_version     = var.postgresql_version
  postgresql_sku         = var.postgresql_sku
  storage_mb             = var.postgres_storage_mb
  tags                   = local.tags
}

module "container_app" {
  source               = "./container-app"
  prefix               = var.prefix
  environment          = var.environment
  service              = var.service
  location             = var.location
  resource_group_name  = module.resource_group.resource_group_name
  container_app_env_id = module.container_app_environment.env_id
  image                = "amilidevmypageregistry.azurecr.io/amili-mypage-api:v1"
  registry_server      = module.container_registry.login_server
  registry_username    = module.container_registry.admin_username
  registry_password    = module.container_registry.admin_password
  tags                 = local.tags

  app_settings = {
    "ConnectionStrings__DefaultConnection" = local.database_connection_string
    "ASPNETCORE_ENVIRONMENT"               = var.environment
  }
}

module "app_service_plan" {
  source              = "./app-service-plan"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.app_service_location
  resource_group_name = module.resource_group.resource_group_name
  tags                = local.tags
}

module "app_service" {
  source              = "./app-service"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  resource_group_name = module.resource_group.resource_group_name
  location            = var.app_service_location
  service_plan_id     = module.app_service_plan.plan_id
  tags                = local.tags

  app_settings = {
    "ConnectionStrings__DefaultConnection" = local.database_connection_string
    "ASPNETCORE_ENVIRONMENT"               = var.environment == "prod" ? "Production" : "Development"
  }
}
