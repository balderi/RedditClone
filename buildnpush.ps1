docker build --rm -t redlab/boards:latest . --platform=linux/arm64 --no-cache
docker tag redlab/boards registry.redlabsolutions.net/boards
docker push registry.redlabsolutions.net/boards