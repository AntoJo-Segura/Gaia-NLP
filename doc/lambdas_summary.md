# Lambda GenerateOperation

## Input: 

### Header

```json
{ 
    "Content-Type:": " multipart/form-data;"
}
```

### Body

```json
{ 
    "document": "file"
}
```

## Output:

```json
{
  "operationId": "c2bb55af-8495-4e1a-9d9b-889c4bb26b46",
  "status":"pending",
  "inputFile":"url-file-s3"
 }
```

```json
{
  "operationId": "c2bb55af-8495-4e1a-9d9b-889c4bb26b46",
  "status":"processed",
  "inputFile":"url-file-s3",
  "outputFile":"url-file-s3"
 }
```

## Algorithm:

Here lambda Algorithm.

--------------------------------------------------------

# Lambda OperationState

## Input: 

http://localhost:5000/operations/{operationId}
http://localhost:5000/operations/4c23a15e-c2a5-40db-813c-a10eeb3201c2


## Output:

```json
{
  "operationId": "c2bb55af-8495-4e1a-9d9b-889c4bb26b46",
  "status":"pending",
  "inputFile":"url-file-s3"
 }
```

```json
{
  "operationId": "c2bb55af-8495-4e1a-9d9b-889c4bb26b46",
  "status":"processed",
  "inputFile":"url-file-s3",
  "outputFile":"url-file-s3"
 }
```

## Algorithm:

Here lambda Algorithm.

--------------------------------------------------------

# Processor
## Input: 

```json
{ "DynamoDBStream": "*" }
```

## Output:

```json
{
  "cpvs": {
    "cpv_1": {
        "pos1":"val1",
        "pos2":"val2",
        "pos3":"val3"
    },
    "cpv_2": {
        "pos3":"val3",
        "pos1":"val1",
        "pos2":"val2"
    }
  }
 }
```

Como algoritmo de ordenación de cpvs dentro del JSON se utiliza la suma de los 5 mayores valores del cpv.

## Algorithm:

- Consulta el txt en s3
- Cosultar los cpv en dynamoDB
- Consulta los parámetros desde parametreStore (número de divisiones del texto, número de cpv por Lambda processor 2)
- Divide el texto (número de divisiones del texto)
- Divide los cpvs (número de cpv por Lambda processor 2)
- Ejecuta el siguiente nivel de lambdas
- Une los resultados en un solo JSON
- Subir JSON en s3 
- Actualizar registro en DynamoDB (Status: procesed , outputFile: url-file-s3)
    - YYYY/MM/DD
        - operationId.txt
- Actualizar registro en DynamoDB (Status: error)
--------------------------------------------------------

# TextAnalytics

## Input: 

```json
{ 
    "text": "......",
    "partitionText": 200, 
    "cpvs": [
    {
        "code":"code1",
        "desc": [
            "desc1",
            "desc2",
            "desc3",
            "desc4"
        ]
    },
    {
        "code":"code2",
        "desc": [
            "desc1",
            "desc2",
            "desc3",
            "desc4"
        ]
    }
    ]
 }
```

## Output:

```json
{
  "cpvs": {
    "cpv_1": {
        "pos1":"acc1",
        "pos2":"acc2",
        "pos3":"acc3"
    },
    "cpv_2": {
        "pos1":"acc1",
        "pos2":"acc2"
    }
  }
 }
```

## Algorithm:

- Recorrer todos los cpvs
- Calculamos el match con la posición absoluta de cada cpv con todo "text"
- Filtrar posiciones poco relevante
- Generar output con todos los resultados

--------------------------------------------------------