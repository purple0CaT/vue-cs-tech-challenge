<template>
	<div>
		<v-card rounded="xl">
			<v-card-text class="d-flex flex-column flex-md-row pa-6">
				<div>
					<v-img
						cover
						rounded="xl"
						:src="fallbackImgAddrs"
						width="300px" />
				</div>
				<div class="flex-grow-1 d-flex flex-column ga-4 px-md-10 pt-4 pt-md-0">
					<h3 class="text-h3">{{ details.name }}</h3>
					<div class="d-flex" />
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Birth year:</strong>
						<span class="text-uppercase">
							{{ details.birth_year }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Gender:</strong>
						<span class="text-capitalize">
							{{ details.gender }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Height:</strong>
						<span class="text-capitalize">
							{{ details.height }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Mass:</strong>
						<span class="text-capitalize">
							{{ details.mass }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Skin color:</strong>
						<span class="text-capitalize">
							{{ details.skin_color }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Eye color:</strong>
						<span class="text-capitalize">
							{{ details.eye_color }}
						</span>
					</p>
					<p class="text-body-1">
						<strong class="text-grey-darken-1 mr-4">Hair color:</strong>
						<span class="text-capitalize">
							{{ details.hair_color }}
						</span>
					</p>
				</div>
			</v-card-text>
		</v-card>
	</div>
</template>

<script setup lang="ts">
import type { StarWarsApiResponse } from '@/api/models/starwars-api.models';
const fallbackImgAddrs =
	'https://www.photomural.com/media/catalog/product/import/api-v1.1-file-public-files-pim-assets-97-ad-84-62-6284ad972eff292d45ce1a2e-images-47-73-f0-65-65f073472bc4ca5854c10e43-008-dvd2-star-wars-poster-classic-2-web.jpg';

const props = defineProps<{
	details: StarWarsApiResponse.Character;
}>();
</script>

<style scoped></style>

