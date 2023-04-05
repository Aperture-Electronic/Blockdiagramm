export function fetchWithTimeout(fetch: Promise<Response>, timeout: number) {
  const fetchPromise = fetch;
  const aboutPromise = new Promise((resolve, reject) => {
    setTimeout(() => {
      reject('Fetch request timed out');
    }, timeout);
  });

  const combinedPromise = Promise.race([fetchPromise, aboutPromise]);
  return combinedPromise;
}

export function fetchJSONWithTimeout(
  fetch: Promise<Response>,
  timeout: number
) {
  return fetchWithTimeout(fetch, timeout).then((response) => {
    if (response instanceof Response) {
      if (response.status === 200) {
        return response.json();
      }
    }
  });
}

export async function fetchJSON(fetch: Promise<Response>) {
  const response = await fetch;
  const json = await response.json();
  return json;
}

export async function fetchJsonPostObject(address: string, request: object) {
  const response = await fetchJSON(
    fetch(address, {
      method: 'POST',
      body: JSON.stringify(request),
      headers: {
        'Content-Type': 'application/json',
      },
    })
  );

  return response;
}

export async function fetchGetObject(address: string, sessionId: string) {
  const response = await fetch(address + '?sessionId=' + sessionId, {
    method: 'GET',
  });

  return response;
}

export async function fetchJsonGetObject(address: string, sessionId: string) {
  const response = await fetchJSON(
    fetch(address + '?sessionId=' + sessionId, {
      method: 'GET',
    })
  );

  return response;
}
