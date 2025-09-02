
install:
	cd apps/vue && yarn install

vue:
	cd apps/vue && yarn dev

build:
	cd apps/vue && yarn build

.PHONY: install vue build

