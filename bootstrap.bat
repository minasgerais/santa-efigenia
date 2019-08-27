
docker stop sta_efigenia_mongo_db sta_efigenia_mongo_express sta_efigenia_rabbitmq sta_efigenia_sauron

docker container rm sta_efigenia_mongo_db sta_efigenia_mongo_express sta_efigenia_rabbitmq sta_efigenia_sauron

docker network rm sta_efigenia_network

#docker network create --gateway 172.16.1.1 --subnet 172.16.1.0/24 sta_efigenia_network

#docker-compose -f ./docker/docker-compose.yml up -d
