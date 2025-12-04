# PlayerShooting - Explicación Detallada

## Descripción General
`PlayerShooting.cs` es un script de Unity que controla el sistema de apuntado y disparo del jugador. Utiliza un joystick para mover una mira (Sign) y rota la mano del jugador para que apunte hacia esa mira.

---

## Variables Públicas

### `public Joystick joystick;`
- **Tipo:** Componente Joystick
- **Propósito:** Referencia al joystick que controla la mira
- **Uso:** Obtiene input horizontal (`joystick.Horizontal`) y vertical (`joystick.Vertical`) para mover la mira

### `public Rigidbody2D rb;`
- **Tipo:** Rigidbody2D
- **Propósito:** Componente de física del objeto Sign
- **Uso:** Permite mover la mira usando física (`rb.MovePosition()`)

### `public Transform Sign;`
- **Tipo:** Transform
- **Propósito:** Referencia al GameObject que representa la mira/cursor de apuntado
- **Uso:** Se mueve en base al input del joystick para indicar hacia dónde apunta el jugador

### `public Transform Hand;`
- **Tipo:** Transform
- **Propósito:** Referencia a la mano del jugador (que sostiene el arma)
- **Uso:** Se rota para que siempre apunte hacia la posición del Sign

### `public GameObject Bullet;`
- **Tipo:** GameObject (Prefab)
- **Propósito:** Prefab de la bala que se instanciará al disparar
- **Uso:** Se clona cuando el jugador dispara

### `public Transform Tip;`
- **Tipo:** Transform
- **Propósito:** Punto desde donde salen las balas (punta del arma)
- **Uso:** Posición donde se instancian las nuevas balas

---

## Métodos

### `void Start()`
- **Cuándo se ejecuta:** Una vez al inicio, antes del primer frame
- **Contenido actual:** Vacío
- **Propósito:** Inicialización (actualmente no usado)

### `void Update()`
- **Cuándo se ejecuta:** Cada frame (~60 veces por segundo)
- **Contenido actual:** Vacío
- **Propósito:** Actualización por frame (actualmente no usado)

---

## FixedUpdate() - Lógica Principal

### ¿Cuándo se ejecuta?
Se ejecuta en intervalos fijos de tiempo (por defecto 50 veces por segundo), sincronizado con el motor de física de Unity.

### Paso 1: Obtener Input del Joystick
```csharp
Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical);
```
- Crea un vector 2D con los valores del joystick
- `joystick.Horizontal`: Movimiento izquierda/derecha (-1 a +1)
- `joystick.Vertical`: Movimiento arriba/abajo (-1 a +1)

### Paso 2: Calcular Nueva Posición de la Mira
```csharp
Vector2 newPosition = (Vector2)Sign.position + input * 30 * Time.deltaTime;
```
- **`Sign.position`**: Posición actual de la mira
- **`input * 30`**: Multiplica el input por velocidad (30 unidades/segundo)
- **`* Time.deltaTime`**: Normaliza para que sea independiente del framerate
- **Resultado:** Nueva posición basada en el movimiento del joystick

### Paso 3: Limitar Movimiento (Clamp)
```csharp
newPosition.x = Mathf.Clamp(newPosition.x, -9.8f, +9.8f);
newPosition.y = Mathf.Clamp(newPosition.y, -4.17f, +4.549f);
```
- **`Mathf.Clamp(valor, min, max)`**: Limita un valor entre un mínimo y máximo
- **Eje X:** Restringido entre -9.8 y +9.8 (ancho del canvas)
- **Eje Y:** Restringido entre -4.17 y +4.549 (alto del canvas)
- **Propósito:** Evita que la mira se salga de los límites de la pantalla

### Paso 4: Aplicar Movimiento con Física
```csharp
rb.MovePosition(newPosition);
```
- Mueve el Rigidbody2D a la nueva posición calculada
- Usa el sistema de física de Unity (más suave que `transform.position`)

### Paso 5: Calcular Dirección Relativa
```csharp
Vector3 relative = Hand.InverseTransformPoint(Sign.position);
```
- **`InverseTransformPoint`**: Convierte la posición del Sign del espacio mundial al espacio local de Hand
- **Resultado:** Posición del Sign relativa a Hand (como si Hand estuviera en el origen)
- **Propósito:** Necesario para calcular el ángulo correcto de rotación

### Paso 6: Calcular Ángulo de Rotación
```csharp
float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
```
- **`Mathf.Atan2(y, x)`**: Calcula el ángulo en radianes usando arctangente de dos variables
- **`* Mathf.Rad2Deg`**: Convierte radianes a grados (Unity usa grados para Rotate)
- **Resultado:** Ángulo en grados que indica la dirección de Hand hacia Sign

### Paso 7: Rotar la Mano
```csharp
Hand.Rotate(0, 0, angle);
```
- Rota Hand en el eje Z (rotación 2D)
- **Parámetros:** `(x, y, z)` → `(0, 0, angle)`
- **Resultado:** La mano apunta hacia donde está la mira

---

## Shooting() - Método de Disparo

### ¿Cuándo se ejecuta?
Este método público debe ser llamado externamente (probablemente por un botón de disparo en el UI).

### Código
```csharp
public void Shooting(){
    GameObject bullet = Instantiate(Bullet, Tip, Quaternion.identity);
}
```

### Parámetros de Instantiate
1. **`Bullet`**: Prefab a clonar
2. **`Tip`**: Parent del objeto instanciado (hijo de Tip)
3. **`Quaternion.identity`**: Sin rotación adicional

### Problema Potencial
⚠️ **Nota:** La bala se crea como hijo de `Tip`, lo que significa:
- Se moverá con el Tip
- Podría no tener rotación correcta
- Debería instanciarse en la posición de Tip pero sin parent

### Solución Sugerida
```csharp
GameObject bullet = Instantiate(Bullet, Tip.position, Tip.rotation);
```
Esto crearía la bala en la posición y rotación del Tip, pero como objeto independiente.

---

## Flujo de Ejecución (Resumen)

1. **Cada FixedUpdate (50 veces/seg):**
   - Lee input del joystick
   - Mueve la mira (Sign) según el input
   - Limita la mira dentro del canvas
   - Calcula el ángulo entre Hand y Sign
   - Rota Hand para que apunte a Sign

2. **Cuando se llama Shooting():**
   - Instancia una bala en la posición del Tip

---

## Diagramas

### Jerarquía de GameObjects
```
Player
├── Hand (se rota)
│   └── Tip (posición de spawn de balas)
└── Sign (mira que se mueve con joystick)
```

### Sistema de Coordenadas
```
Canvas Bounds:
X: -9.8 ←──────→ +9.8
Y: -4.17 ↑ +4.549

Sign se mueve dentro de estos límites
```

---

## Conceptos Técnicos

### FixedUpdate vs Update
- **Update:** Se ejecuta cada frame (variable)
- **FixedUpdate:** Se ejecuta en intervalos fijos (mejor para física)
- **Este script usa FixedUpdate** porque usa Rigidbody2D

### InverseTransformPoint
Convierte coordenadas globales a locales:
- **Mundo:** Sign está en (5, 3)
- **Hand:** Está en (2, 1)
- **Relativo:** Sign está en (3, 2) respecto a Hand

### Atan2
Calcula el ángulo de un vector:
- Input: (x, y) → componentes del vector
- Output: ángulo en radianes (-π a +π)
- Ventaja sobre Atan: Funciona en todos los cuadrantes

---

## Valores Importantes

| Variable | Valor | Descripción |
|----------|-------|-------------|
| Velocidad de mira | 30 | Unidades/segundo |
| Límite X | -9.8 a +9.8 | Ancho del canvas |
| Límite Y | -4.17 a +4.549 | Alto del canvas |

---

## Mejoras Sugeridas

1. **Shooting Method:**
   - Cambiar parent de Tip a null
   - Aplicar rotación correcta a la bala
   - Agregar velocidad a la bala

2. **Optimización:**
   - Cachear `Mathf.Rad2Deg` como constante
   - Evitar `Rotate` cada frame (usar `rotation` directamente)

3. **Control:**
   - Agregar sensibilidad configurable
   - Agregar smoothing al movimiento de la mira
