/* eslint-disable react-refresh/only-export-components */
import { Link, createFileRoute } from "@tanstack/react-router";

const Landing = () => (
  <main className="landing-page">
    <section className="landing-hero">
      <p className="eyebrow">Лабораторная работа 8</p>
      <h1>TanStack Router + Query</h1>
      
      <div className="landing-actions">
        <Link to="/catalog" className="primary-link">
          Открыть каталог
        </Link>
        <Link to="/login" className="secondary-link">
          Авторизация
        </Link>
      </div>
    </section>
  </main>
);

export const Route = createFileRoute("/")({
  component: Landing
});
