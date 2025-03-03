#!/bin/bash
# wait-for-it.sh
# Verifica si el servicio MongoDB está disponible antes de iniciar la aplicación

HOST=$1
PORT=$2
shift
shift

until nc -z -v -w30 $HOST $PORT
do
  echo "Esperando a que MongoDB esté disponible..."
  sleep 5
done

echo "MongoDB está disponible, arrancando la aplicación..."
exec "$@"
