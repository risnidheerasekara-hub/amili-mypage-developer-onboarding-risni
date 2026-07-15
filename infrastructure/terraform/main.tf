locals {
  tags = {
    product     = var.prefix
    environment = var.environment
    service     = var.service
  }

  database_connection_string = "Host=${module.postgres_database.server_fqdn};Port=5432;Database=${module.postgres_database.database_name};Username=${module.postgres_database.admin_login};Password=${var.postgres_admin_password};"
}

module "resource_group" {
  source      = "./resource-group"
  prefix      = var.prefix
  environment = var.environment
  service     = var.service
  location    = var.location
  tags        = local.tags
}

module "postgres_database" {
  source              = "./postgres-database"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.resource_group_name
  admin_password      = var.postgres_admin_password
  tags                = local.tags
}

module "app_service_plan" {
  source              = "./app-service-plan"
  prefix              = var.prefix
  environment         = var.environment
  service             = var.service
  location            = var.location
  resource_group_name = module.resource_group.resource_group_name
  tags                = local.tags
}

module "backend_app" {
  source              = "./app-service"
  name                = "${var.prefix}-${var.environment}-${var.service}-api"
  resource_group_name = module.resource_group.resource_group_name
  location            = var.location
  service_plan_id     = module.app_service_plan.plan_id
  runtime             = "dotnet"
  tags                = local.tags

  app_settings = {
    "ConnectionStrings__DefaultConnection" = local.database_connection_string
    "ASPNETCORE_ENVIRONMENT"               = var.environment == "prod" ? "Production" : "Development"
  }
}

module "frontend_app" {
  source              = "./app-service"
  name                = "${var.prefix}-${var.environment}-${var.service}-web"
  resource_group_name = module.resource_group.resource_group_name
  location            = var.location
  service_plan_id     = module.app_service_plan.plan_id
  runtime             = "node"
  app_command_line    = "npx serve -s . -l $PORT"
  tags                = local.tags

  app_settings = {
    "SCM_DO_BUILD_DURING_DEPLOYMENT" = "false"
  }
}
