output "frontend_url" {
  description = "Public URL of the Static Web App (your frontend)"
  value       = "https://${module.static_web_app.default_hostname}"
}

output "backend_url" {
  description = "Public URL of the App Service (your API)"
  value       = "https://${module.app_service.default_hostname}"
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