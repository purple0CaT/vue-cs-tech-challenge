
yarn:
	cd apps/vue && yarn install

vue:
	cd apps/vue && yarn dev

yarn-vue:
	cd apps/vue && yarn install && yarn dev

build:
	cd apps/vue && yarn build


npm-i:
	cd apps/vue && npm install

npm-vue:
	cd apps/vue && npm run dev

npm-i-vue:
	cd apps/vue && npm install && npm run dev

npm-build:
	cd apps/vue && npm run build

dotnet:
	cd apps/ApiCore && dotnet restore && cd Api/ApiCore.Api && dotnet run

.PHONY: yarn vue build npm-i npm-dev npm-build
