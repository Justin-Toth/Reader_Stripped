# UFID Reader Frontend

This is the frontend application for the UFID Reader project, built using Nuxt 3. It currently serves as the user interface for managing and interacting with the database.

For further info on Nuxt go [here](https://nuxt.com/)

## Prerequisites

- [Node](https://nodejs.org/en/download)

## Setup

1. CD into the frontend folder.

2. Make sure to install dependencies:

```bash
# npm
npm install

# pnpm
pnpm install

# yarn
yarn install

# bun
bun install
```

## Running the Development Server

Start the development server on `http://localhost:3000`:

```bash
# npm
npm run dev

# pnpm
pnpm dev

# yarn
yarn dev

# bun
bun run dev
```

## Building the application for production:

```bash
# npm
npm run build

# pnpm
pnpm build

# yarn
yarn build

# bun
bun run build
```

## Locally preview production build:

```bash
# npm
npm run preview

# pnpm
pnpm preview

# yarn
yarn preview

# bun
bun run preview
```

Check out the [deployment documentation](https://nuxt.com/docs/getting-started/deployment) for more information.

---

## Frontend Structure

The frontend is organized into the following structure:

- **`app.vue`**: The root component that defines the application layout.
- **`components/`**: Contains reusable Vue components, including UI elements and layout components.
  - **`ui/`**: Houses UI components built using `shadcn-vue`, a library for building accessible and customizable UI components.
- **`layouts/`**: Defines application layouts, such as `default.vue` for authenticated pages and `noAuth.vue` for unauthenticated pages.
- **`pages/`**: Contains the main pages of the application. Each `.vue` file corresponds to a route.
  - Example: `index.vue` is the dashboard, `students.vue` manages students, and `kiosks.vue` manages kiosks.
- **`assets/`**: Stores static assets like CSS files and images.
- **`public/`**: Contains public files served directly, such as `favicon.ico`.
- **`nuxt.config.ts`**: The main configuration file for the Nuxt application.
- **`tailwind.config.js`**: Tailwind CSS configuration for styling.
- **`composables/`**: Houses reusable logic, such as API hooks for students and kiosks.
- **`lib/`**: Contains utility functions used across the application.

## Future Recommendations

1. **Code Quality**:

   - Refactor components to ensure consistency in naming and structure.

2. **Testing**:

   - Add unit tests for components and composables using a testing framework like Vitest or Jest.

3. **Documentation**:

   - Expand this README with detailed examples for using components and composables.
   - Add inline comments to complex logic for better maintainability.

4. **Styling**:

   - Standardize Tailwind CSS usage across components.
   - Document the design system (e.g., colors, spacing, typography) for consistency.

5. **Deployment**:
   - Automate deployment using CI/CD pipelines.
   - Containerize the application with Docker for consistent deployment environments.
