<template>
	<header class="bg-primary d-flex justify-center align-center">
		<router-link
			style="height: 35px; width: 35px"
			to="/">
			<section class="logo-flip">
				<div class="logo-flip__inner">
					<div class="logo-flip-front">
						<v-img
							alt="Empire SVG"
							class="cursor-pointer error"
							cover
							src="/svgs/empire.svg"
							style="height: 35px" />
					</div>
					<div class="logo-flip-back">
						<v-img
							alt="Rebelion SVG"
							class="cursor-pointer"
							src="/svgs/rebelion.svg"
							style="height: 35px" />
					</div>
				</div>
			</section>
		</router-link>

		<section
			class="absolute"
			style="right: 8px; top: 8px">
			<div>
				<v-tooltip
					activator="parent"
					location="start">
					{{ healthy ? 'The server is healthy' : 'The server is down' }}
				</v-tooltip>
				<v-icon
					v-if="healthy"
					color="success">
					mdi-wifi-strength-4
				</v-icon>
				<v-icon
					v-else
					color="error">
					mdi-wifi-strength-off
				</v-icon>
			</div>
		</section>
	</header>
</template>

<script setup>
import { useHealthStore } from '@/store/stores/health.store';

const healthStore = useHealthStore();
const { healthy } = storeToRefs(healthStore);

const interval = ref(null);

onMounted(() => {
	healthStore.checkHealth();
	interval.value = setInterval(() => {
		healthStore.checkHealth();
	}, 15 * 1000);
});
onBeforeUnmount(() => {
	clearInterval(interval.value);
});
</script>

<style lang="scss" scoped>
header {
	height: 50px;
	display: flex;
	justify-content: space-between;
	align-items: center;
	padding: 8px;
}

.logo-flip {
	position: absolute;
	width: 35px;
	height: 35px;
	perspective: 500px;
	&__inner {
		position: absolute;
		width: 100%;
		height: 100%;
		transition: transform 1s;
		transform-style: preserve-3d;
	}
	&:hover {
		& .logo-flip__inner {
			transform: rotateY(180deg);
			transition: transform 0.5s;
		}
	}
}
.logo-flip-front,
.logo-flip-back {
	position: absolute;
	height: 100%;
	width: 100%;
	text-align: center;
	backface-visibility: hidden;
}
.logo-flip-back {
	transform: rotateY(180deg);
}
</style>

