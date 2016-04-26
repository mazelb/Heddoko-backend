# heddoko-backend

0. clear redis
redis-cli KEYS "dev:*"

redis-cli KEYS "dev:*" | xargs redis-cli DEL

1. Revert to first Migration
Update-Database –TargetMigration: $InitialDatabase