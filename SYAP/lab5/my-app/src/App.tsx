import React, { useState } from 'react';
import Button from './components/ui/Button/Button';
import Input from './components/ui/Input/Input';
import Badge from './components/ui/Badge/Badge';
import LayoutCard from './components/ui/LayoutCard/LayoutCard';
import './App.css';

function App() {
  const [inputValue, setInputValue] = useState('');
  const [inputError, setInputError] = useState('');
  const [emailValue, setEmailValue] = useState('');
  const [emailError, setEmailError] = useState('');

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setInputValue(value);
    
    if (value.length > 0 && value.length < 3) {
      setInputError('Минимум 3 символа');
    } else {
      setInputError('');
    }
  };

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setEmailValue(value);
    
    if (value && !value.includes('@')) {
      setEmailError('Email должен содержать символ @');
    } else {
      setEmailError('');
    }
  };
  return (
    <div className="app" style={{ padding: '20px', maxWidth: '1200px', margin: '0 auto' }}>
      <h1>UI Kit</h1>
      
      {/* Кнопки */}
      <LayoutCard title="Кнопки">
        <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
          <Button variant="primary" size="small">Primary Small</Button>
          <Button variant="primary" size="medium">Primary Medium</Button>
          <Button variant="primary" size="large">Primary Large</Button>
          <Button variant="secondary">Secondary</Button>
          <Button variant="danger">Danger</Button>
          <Button isLoading>Loading</Button>
          <Button disabled>Disabled</Button>
        </div>
      </LayoutCard>

      {/* Поля ввода */}
      <LayoutCard title="Поля ввода">
        <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
          <Input
            label="Имя пользователя"
            placeholder="Введите имя"
            value={inputValue}
            onChange={handleInputChange}
            error={inputError}
          />
          <Input
            label="Email"
            placeholder="user@example.com"
            type="email"
            value={emailValue}
            onChange={handleEmailChange}
            error={emailError}
            isFullWidth
          />
          <Input
            label="Пароль"
            type="password"
            placeholder="Введите пароль"
          />
        </div>
      </LayoutCard>

      {/* Бейджи */}
      <LayoutCard title="Бейджи">
        <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
          <Badge color="green" text="Online" />
          <Badge color="red" text="Offline" />
          <Badge color="orange" text="Pending" />
          <Badge color="blue" text="Admin" />
        </div>
      </LayoutCard>
    </div>
  );
}

export default App;