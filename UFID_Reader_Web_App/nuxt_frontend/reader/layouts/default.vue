<template>
    <div class="flex min-h-screen w-full flex-col bg-muted/40">
        <!-- Sidebar-->
        <Sidebar />

        <div class="flex flex-col sm:gap-4 sm:py-4 sm:pl-[150px]">
            <!--Header-->
            <Header />

            <!--Page Content-->
            <slot />
        </div>
    </div>
</template>