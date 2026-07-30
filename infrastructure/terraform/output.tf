output "frontend_url" {
  description = "Public URL of the Static Web App (your frontend)"
  value       = "https://${module.static_web_app.default_hostname}"
}

output "backend_url" {
  description = "Public URL of the App Service (your API)"
  value       = "https://${module.backend_app.default_hostname}"
}

output "static_web_app_deployment_token" {
  description = "Token used by GitHub Actions to deploy to the Static Web App"
  value       = module.static_web_app.deployment_token
  sensitive   = true 
}

output "postgres_server_fqdn" {
  description = "Fully qualified domain name of the PostgreSQL server"
  value       = module.postgres_server.server_fqdn
}

output "acr_login_server" {
  description = "Azure Container Registry login server"
  value       = module.container_registry.login_server
}

output "acr_admin_username" {
  description = "ACR admin username for docker login"
  value       = module.container_registry.admin_username
  sensitive   = true
}

output "acr_admin_password" {
  description = "ACR admin password for docker login"
  value       = module.container_registry.admin_password
  sensitive   = true
}