<template>
	<section class="flex-grow-1 d-flex flex-column justify-center align-center">
		<v-card rounded="xl">
			<v-card-text class="d-flex flex-column align-center ga-3">
				<v-img
					width="100%"
					max-width="350px"
					src="/svgs/death-star.svg"
					cover />
				<span class="text-h4 text-error">404</span>
				<span class="text-h6 text-grey-darken-1">
					The page you are looking for does not exist
				</span>

				<v-divider class="w-100"></v-divider>
				<div>
					<v-btn
						color="primary"
						variant="outlined"
						rounded="pill"
						width="150px"
						to="/">
						Go Home
					</v-btn>
				</div>
			</v-card-text>
		</v-card>
	</section>
</template>

<script setup lang="ts"></script>

<style scoped></style>

